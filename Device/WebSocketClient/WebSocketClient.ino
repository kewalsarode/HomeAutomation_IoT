/*
   WebSocketClient.ino

    Created on: 24.05.2015

*/

#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <DNSServer.h>
#include <ESP8266WebServer.h>
#include <WiFiManager.h>
#include <WebSocketsClient.h>
#include <Hash.h>
#include <ArduinoJson.h>

WebSocketsClient webSocket;
WiFiManager wifiManager;

#define USE_SERIAL Serial1

void webSocketEvent(WStype_t type, uint8_t * payload, size_t length) {

  switch (type) {
    case WStype_DISCONNECTED:
      break;
    case WStype_CONNECTED: {
        // send message to server when Connected
        webSocket.sendTXT("ESPConnected");
      }
      break;
    case WStype_TEXT:
      {
        StaticJsonDocument<200> doc;

        // Test if parsing succeeds.
        if (deserializeJson(doc, (char *)payload)) {
          Serial.print(F("deserializeJson() failed: "));
          return;
        }

        // Get the root object in the document
        JsonObject root = doc.as<JsonObject>();

        if(strcmp(root["Action"],"STATUS")==0) {
          String strStatus = "{\"Switch\":";
          strStatus += digitalRead(0);
          strStatus += ", \"Fan\":";
          strStatus += digitalRead(2);
          strStatus += "}";
          webSocket.sendTXT(strStatus);
          }

        if (strcmp(root["Action"], "RESET") == 0) {
          wifiManager.resetSettings();
        }

        // Parameters
        //const char* deviceType = root["DeviceType"];
        if (strcmp(root["DeviceType"], "Switch") == 0)
        {
          if (strcmp(root["Action"], "ON") == 0)
            digitalWrite(0, LOW);
          if (strcmp(root["Action"], "OFF") == 0)
            digitalWrite(0, HIGH);
        }
        if (strcmp(root["DeviceType"], "Fan") == 0)
        {
          if (strcmp(root["Action"], "ON") == 0)
            digitalWrite(2, LOW);
          if (strcmp(root["Action"], "OFF") == 0)
            digitalWrite(2, HIGH);
        }
      }
      break;
    case WStype_BIN:
      hexdump(payload, length);
      // send data to server
      webSocket.sendBIN(payload, length);
      break;
  }
}

void setup() {
  USE_SERIAL.begin(115200);
  wifiManager.autoConnect("AutoHomeConnect");

  pinMode(0, OUTPUT);
  pinMode(2, OUTPUT);

  digitalWrite(0, HIGH);
  digitalWrite(2, HIGH);
  //Serial.setDebugOutput(true);
  //USE_SERIAL.setDebugOutput(true);

  for (uint8_t t = 4; t > 0; t--) {
    USE_SERIAL.flush();
    delay(1000);
  }

  // server address, port and URL
  //webSocket.begin("192.168.1.101", 8011, "/");
  webSocket.begin("smarterhome.azurewebsites.net", 80, "/api/Device/Connect");
  webSocket.setExtraHeaders("device-serial: E2C6EA47551B");
  // event handler
  webSocket.onEvent(webSocketEvent);
  // use HTTP Basic Authorization this is optional remove if not needed
  //webSocket.setAuthorization("user", "Password");
  // try ever 5000 again if connection has failed
  webSocket.setReconnectInterval(5000);
}

void loop() {
  webSocket.loop();
}
