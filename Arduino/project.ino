#include <SoftwareSerial.h>
#define BT_RXD 3
#define BT_TXD 2

int moter_d[2] = { 13, 11 };
int moter_b[2] = { 12, 10 };

int milliseconds = 1000;

typedef enum {
  Stop = 0b0000,
  Forward = 0b0001,
  Left = 0b0010,
  Right = 0b0100,
  Back = 0b1000
} direction;

typedef enum {
  Direct = 0b0001,
  Reverse = 0b0100
} moterState;


SoftwareSerial bluetooth(BT_RXD, BT_TXD);

void setup() {
  setting();
}

void loop() {
  communication();
}

void setting() {
  Serial.begin(9600);
  bluetooth.begin(9600);

  for (int i = 0; i < 2; i++) {
    pinMode(moter_d[i], OUTPUT);
    pinMode(moter_b[i], OUTPUT);
  }
}

void communication() {
  if (bluetooth.available()) {
    move(bluetooth.read());
  }
  if (Serial.available()) {
    bluetooth.write(Serial.read());
  }
}

void setMoter(int m, int dir) {
  if (dir == Stop) { // 정지
    digitalWrite(moter_d[m], LOW);
    digitalWrite(moter_b[m], LOW);
  } 
  else if (dir == Direct) { // 전진
    digitalWrite(moter_d[m], HIGH);
    digitalWrite(moter_b[m], LOW);
  }
  else if (dir == Reverse) { // 후진
    digitalWrite(moter_d[m], LOW);
    digitalWrite(moter_b[m], HIGH);
  }
}

void move(int dir) {
  if (dir == Stop) {
    setMoter(0, Stop);
    setMoter(1, Stop);
  }
  else if (dir == Forward) { // 앞쪽
    setMoter(0, Direct);
    setMoter(1, Direct);
  }
  else if (dir == Left) { // 왼쪽
    setMoter(0, Reverse);
    setMoter(1, Direct);
  }
  else if (dir == Right) { // 오른쪽
    setMoter(0, Direct);
    setMoter(1, Reverse);
  }
  else if (dir == Back) { // 뒷쪽
    setMoter(0, Reverse);
    setMoter(1, Reverse);
  }
  else if (dir == Forward | Left) { // 왼쪽 앞
    setMoter(0, Direct);
    setMoter(1, Stop);
  }
  else if (dir == Forward | Right) { // 오른쪽 앞
    setMoter(0, Stop);
    setMoter(1, Direct);
  }
  else if (dir == Back | Left) { // 왼쪽 뒤
    setMoter(0, Reverse);
    setMoter(1, Stop);
  }
  else if (dir == Back | Right) { // 오른쪽 뒤
    setMoter(0, Stop);
    setMoter(1, Reverse);
  }

  delay(milliseconds);

  setMoter(0, Stop);
  setMoter(1, Stop);
}

