#include "SerialCommunication.h";
String incomingMessage = "";
String message = "";
bool gettingMessage = false;

bool fullIncomingMessage() {
  if (Serial.available() > 0) {
    int incomingByte = Serial.read();
    char incomingChar = (char)incomingByte;
    if (incomingChar == '%')
    {
      message = incomingMessage;
      gettingMessage = false;
      return true;
    }
    if (gettingMessage) {
      incomingMessage += incomingChar;
    }
    if (incomingChar == '#') {
      gettingMessage = true;
      incomingMessage = "";
    }
  }
  return false;
}

String getMessage()
{
  return incomingMessage;
}
