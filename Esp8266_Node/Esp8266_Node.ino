#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

#ifndef STASSID
#define STASSID "ESP AP"
#define STAPSK  "yourPassword"
#endif

IPAddress APIP(192, 168, 4, 1);
unsigned int localPort = 8888;      // local port to listen on

// buffers for receiving and sending data
char packetBuffer[UDP_TX_PACKET_MAX_SIZE + 1]; //buffer to hold incoming packet,

WiFiUDP Udp;


const int BUTTON_PIN = 0;
int LED_RED = 1;
int LED_GREEN = 2;
int LED_BLUE = 3;

int lastButtonState = LOW;
char ledStatus = '0';




String Read()
{
  int packetSize = Udp.parsePacket();
  if (packetSize) {
    // read the packet into packetBufffer
    int n = Udp.read(packetBuffer, UDP_TX_PACKET_MAX_SIZE);
    packetBuffer[n] = 0;
    return packetBuffer;
  }
  return "";
}

void Send(char * message)
{
  // send a Send, to the IP address and port that sent us the packet we received
  Udp.beginPacket(APIP, Udp.remotePort());
  Udp.write(message);
  Udp.endPacket();
}


void ControlLed()
{
  if (ledStatus == '0')
  {
    digitalWrite(LED_RED, LOW);
    digitalWrite(LED_GREEN, LOW);
    digitalWrite(LED_BLUE, LOW);
  }
  if (ledStatus == 'R')
  {
    digitalWrite(LED_RED, LOW);
    digitalWrite(LED_GREEN, HIGH);
    digitalWrite(LED_BLUE, HIGH);
  }
  if (ledStatus == 'G')
  {
    digitalWrite(LED_RED, HIGH);
    digitalWrite(LED_GREEN, LOW);
    digitalWrite(LED_BLUE, HIGH);
  }
  if (ledStatus == 'B')
  {
    digitalWrite(LED_RED, HIGH);
    digitalWrite(LED_GREEN, HIGH);
    digitalWrite(LED_BLUE, LOW);
  }
}














void setup() {
  WiFi.mode(WIFI_STA);
  WiFi.begin(STASSID, STAPSK);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
  }
  Udp.begin(localPort);

  pinMode(BUTTON_PIN, INPUT);
  pinMode(LED_RED, OUTPUT);
  pinMode(LED_GREEN, OUTPUT);
  pinMode(LED_BLUE, OUTPUT);
  digitalWrite(LED_RED, LOW);
  digitalWrite(LED_GREEN, LOW);
  digitalWrite(LED_BLUE, LOW);
}

bool setUp = false;
void loop() {
  if (WiFi.status() != WL_CONNECTED)
  {
    setUp = false;
  }
  else
  {
    String message = Read();

    if (message != "")
    {
      if (message == "R")
      {
        ledStatus = 'R';
      }
      if (message == "G")
      {
        ledStatus = 'G';
      }
      if (message == "B")
      {
        ledStatus = 'B';
      }
      if (message == "0")
      {
        ledStatus = '0';
      }
      if (message == "SETUP")
      {
        setUp = true;
      }
    }

    if (setUp)
    {
      ControlLed();

      if (digitalRead(BUTTON_PIN) == HIGH && lastButtonState == LOW)
      {
        Send("Player");
      }
      lastButtonState = digitalRead(BUTTON_PIN);
    }
    else
    {
      Send("HELLO");
      delay(500);
    }

  }
}
