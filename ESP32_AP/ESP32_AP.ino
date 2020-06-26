#include <WiFi.h>
#include <WiFiClient.h>
#include <WiFiAP.h>
#include <WiFiUdp.h>

#include "SerialCommunication.h";

// Set these to your desired credentials.
const char *ssid = "ESP AP";
const char *password = "yourPassword";

//The udp library class
WiFiUDP udp;
char packetBuffer[50]; //buffer to hold incoming packet,

IPAddress broadcastAddress(192, 168, 4, 255);
const int udpPort = 8888;

typedef struct Nodes {
  IPAddress ipAddress;
  char ledStatus = '0';
  bool player = false;
} Node;
IPAddress emptyIP;


// 8 is the maximum amount of nodes this wifi lib. supports
Node nodes[8];
char newLedStatus[8];

int connectedNodes = 1; // starts at 1 if this node is being used as a node else start at 0

//variables neccesary for this to also be used as a node
int LED_RED = 2; //is also internal led for testing
int LED_GREEN = 4;
int LED_BLUE = 16;

int BUTTON_PIN = 22;
int lastButtonState = HIGH;
//////////////////////////////////////////////////////////


unsigned long previousMillis = 0;        // will store last time the network was pinged

const long interval = 1000;           // interval at which to ping

String Read()
{
  int packetSize = udp.parsePacket();
  if ( packetSize)
  {
    // read the packet into packetBufffer
    int n = udp.read(packetBuffer, 50);
    packetBuffer[n] = 0;
    return packetBuffer;
  }
  return "";
}

void Send(char * message, IPAddress ip)
{
  udp.beginPacket(ip, udpPort);
  udp.printf(message);
  udp.endPacket();
}

void AddNode(IPAddress ip)
{
  bool nodeExists = false;
  int nodeNr = connectedNodes;
  for (int i = 0; i < 4; i++)
  {
    if (nodes[i].ipAddress == ip)
    {
      nodeExists = true;
      nodeNr = i;
    }
  }
  if (!nodeExists && connectedNodes < 5)
  {
    nodes[connectedNodes].ipAddress = ip;
    nodeNr = connectedNodes;
    connectedNodes++;
  }
}

void UpdateNodes()
{
  nodes[0].ledStatus = newLedStatus[0]; // if this is a node
  ControlLed(); // if this is a node
  for (int i = 1; i < 5; i++) // change i to 0 if this is not a node
  {
    if (nodes[i].ipAddress != emptyIP && nodes[i].ledStatus != newLedStatus[i])
    {
      nodes[i].ledStatus = newLedStatus[i];
      char message[2];
      message[0] = nodes[i].ledStatus;
      message[1] = '\0';
      Send(message, nodes[i].ipAddress);
    }
  }
}

int GetPlayerLocation()
{
  int location = 0;
  for (int i = 0; i < 5; i++)
  {
    if (nodes[i].player == true)
    {
      location = i;
    }
  }
  return location;
}

void SetPlayerLocation(int nodeNr)
{
  if (GetPlayerLocation() != nodeNr)
  {
    for (int i = 0; i < 5; i++)
    {
      nodes[i].player = false;
    }
    nodes[nodeNr].player = true;
    Serial.print('#'); Serial.print(nodeNr); Serial.println('%');
  }

}

void SetPlayerLocation(IPAddress IP)
{
  int nodeNr = 0;
  for (int i = 1; i < 5; i++)
  {
    if (nodes[i].ipAddress == IP)
    {
      nodeNr = i;
    }
  }
  SetPlayerLocation(nodeNr);
}





void ChangeLedStatus(String values)
{
  char value[5];
  values.toCharArray(value, 5);
  for (int i = 0; i <= 5; i++)
  {
    newLedStatus[i] = value[i];
  }
  UpdateNodes();
}

void SendInfo()
{
  Serial.println("Nodes setup");
  for (int i = 0; i < 5; i++)
  {
    Serial.print(nodes[i].ipAddress); Serial.print(" "); Serial.print(" ");
    Serial.print(nodes[i].ledStatus); Serial.print(" "); Serial.println(nodes[i].player);

  }
}

bool PlayerDetected() // if this is a node
{
  bool detected = false;
  if (digitalRead(BUTTON_PIN) == HIGH && lastButtonState == LOW)
  {
    detected = true;
  }
  lastButtonState = digitalRead(BUTTON_PIN);
  return detected;
}

void ControlLed() // if this is a node
{
  if (nodes[0].ledStatus == '0')
  {
    digitalWrite(LED_RED, LOW);
    digitalWrite(LED_GREEN, LOW);
    digitalWrite(LED_BLUE, LOW);
  }
  if (nodes[0].ledStatus == 'R')
  {
    digitalWrite(LED_RED, HIGH);
    digitalWrite(LED_GREEN, LOW);
    digitalWrite(LED_BLUE, LOW);
  }
  if (nodes[0].ledStatus == 'G')
  {
    digitalWrite(LED_RED, LOW);
    digitalWrite(LED_GREEN, HIGH);
    digitalWrite(LED_BLUE, LOW);
  }
  if (nodes[0].ledStatus == 'B')
  {
    digitalWrite(LED_RED, LOW);
    digitalWrite(LED_GREEN, LOW);
    digitalWrite(LED_BLUE, HIGH);
  }
}


void setup() {
  // You can remove the password parameter if you want the AP to be open.
  Serial.begin(9600);
  WiFi.softAP(ssid, password);
  Serial.println(WiFi.softAPIP());

  // if this is a node
  pinMode(LED_RED, OUTPUT);
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_BLUE, OUTPUT);
  pinMode(BUTTON_PIN, INPUT);
  ////////////////////////////
}



void loop() {
  String message = Read();
  if (message != "")
  {
    if (message == "HELLO")
    {
      Serial.println("node connected");
      AddNode(udp.remoteIP());
      Send("SETUP", udp.remoteIP());
    }
    if (message == "Player")
    {
      SetPlayerLocation(udp.remoteIP());
    }
  }



  if (PlayerDetected()) // if this is a node
  {
    SetPlayerLocation(0);
  }

  if (fullIncomingMessage())
  {
    String usbMessage = getMessage();
    if (usbMessage == "INFO")
    {
      SendInfo();
    }
    if (usbMessage == "REQ")
    {
      Serial.println("#ACK%");
    }
    else
    {
      ChangeLedStatus(usbMessage);
    }
  }

  unsigned long currentMillis = millis();

  if (currentMillis - previousMillis >= interval)
  {
    previousMillis = currentMillis;
    Send(".", broadcastAddress);
  }
}
