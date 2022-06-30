// (c) Michael Schoeffler 2014, http://www.mschoeffler.de

#include "SPI.h" // SPI library
#include "MFRC522.h" // RFID library (https://github.com/miguelbalboa/rfid)

const int pinRST = 9;
const int pinSDA = 10;
const int pinGreenLED = 7;
const int pinRedLED = 4;
const int pinBuzzer = 8;  

MFRC522 mfrc522(pinSDA, pinRST); // Set up mfrc522 on the Arduino

void setup() {
  pinMode(pinGreenLED, OUTPUT);
  pinMode(pinRedLED, OUTPUT);
  pinMode(pinBuzzer, OUTPUT);

  digitalWrite(pinGreenLED, LOW);
  digitalWrite(pinRedLED, HIGH);
  noTone(pinBuzzer);
  
  SPI.begin(); // open SPI connection
  mfrc522.PCD_Init(); // Initialize Proximity Coupling Device (PCD)
  Serial.begin(9600); // open serial connection
}

void alertSuccess(){
  digitalWrite(pinRedLED, LOW);
  digitalWrite(pinGreenLED, HIGH);

  tone(pinBuzzer, 1000);
  delay(500);
  noTone(pinBuzzer);
  
  digitalWrite(pinRedLED, HIGH);
  digitalWrite(pinGreenLED, LOW);
  delay(2500);
  }

 void alertFailed(){
  digitalWrite(pinRedLED, LOW);
  delay(200);

  digitalWrite(pinRedLED, HIGH);
  tone(pinBuzzer, 1000);
  delay(200);

  digitalWrite(pinRedLED, LOW);
  noTone(pinBuzzer);
  delay(200);

  digitalWrite(pinRedLED, HIGH);
  tone(pinBuzzer, 1000);
  delay(200);

  digitalWrite(pinRedLED, LOW);
  noTone(pinBuzzer);
  delay(200);
  
  digitalWrite(pinRedLED, HIGH);
  delay(2000);
  }

void loop() {
  String tagID;
  if (mfrc522.PICC_IsNewCardPresent()) { // (true, if RFID tag/card is present ) PICC = Proximity Integrated Circuit Card
    if(mfrc522.PICC_ReadCardSerial()) { // true, if RFID tag/card was read
      //Serial.print("RFID TAG ID:");
      for (byte i = 0; i < mfrc522.uid.size; ++i) { // read id (in parts)
        tagID += String(mfrc522.uid.uidByte[i], HEX); // print id
      }
       // Print out of id is complete.
      Serial.println(tagID);
      alertSuccess();
    }
  }
}
