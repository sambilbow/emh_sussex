/*
    Analog Input to Unity (Serial)
    for use with Arduino Nano,and Seeed
    Grove Ear-Clip Heart Rate Sensor.

    Sends Analog Inputs via Serial

    Sam Bilbow (c) 2022
    for the Embodiment Hackathon
    at the University of Sussex
    www.sambilbow.com
*/




// ----------
// Libraries
// ----------
#define USE_ARDUINO_INTERRUPTS true
#include <PulseSensorPlayground.h>
PulseSensorPlayground pulseSensor;




// ----------
// Variables
// ----------
// Set the variables for the Heart Rate Sensor
const int OUTPUT_TYPE = SERIAL_PLOTTER; // [!remove?]
const int PULSE_INPUT = A0;     // Analog Pin 0
const int PULSE_BLINK = 13;     // Pin 13 is the on-board LED
const int PULSE_FADE = 5;       // Set time (ms) for LED fade
const int THRESHOLD = 550;     // Threshold to avoid sensor noise when idle
byte samplesUntilReport;
const byte SAMPLES_PER_SERIAL_SAMPLE = 10;




// ----------
// Setup - Initialise Serial / begin Pulse Sensor readings
// ----------
void setup() {
  // Open serial communications and wait for port to open before carrying on
  Serial.begin(250000);
  while (!Serial) {
    ;
  }

  // Configure the PulseSensor manager.
  pulseSensor.analogInput(PULSE_INPUT);
  pulseSensor.blinkOnPulse(PULSE_BLINK);
  pulseSensor.fadeOnPulse(PULSE_FADE);
  pulseSensor.setSerial(Serial);
  pulseSensor.setOutputType(OUTPUT_TYPE);
  pulseSensor.setThreshold(THRESHOLD);

  // Skip the first SAMPLES_PER_SERIAL_SAMPLE in the loop()
  samplesUntilReport = SAMPLES_PER_SERIAL_SAMPLE;

  // Now that everything is ready, start reading the PulseSensor signal
  if (!pulseSensor.begin()) {
    for (;;) {
      // Flash the led to show things didn't work
      digitalWrite(PULSE_BLINK, LOW);
      delay(50);
      digitalWrite(PULSE_BLINK, HIGH);
      delay(50);
    }
  }
}




// ----------
// Loop - Report samples from Heart Rate Sensor, send 3 (heart rate, IBI, pulse) via serial as well as the rest of the analog pins
// ----------
void loop() {
  delay(30);

  // Print Heart Rate Sensor data to serial with /a0/ prepend
  //Serial.print("/a0/");
  Serial.print(pulseSensor.getBeatsPerMinute());
  Serial.print(",");
  Serial.print(pulseSensor.getInterBeatIntervalMs());
  Serial.print(",");
  Serial.print(pulseSensor.getLatestSample());
  Serial.print(",");
  Serial.print(analogRead(1));
  Serial.print(",");
  Serial.print(analogRead(2));
  Serial.print(",");
  Serial.print(analogRead(3));
  Serial.print(",");
  Serial.print(analogRead(4));
  Serial.print(",");
  Serial.print(analogRead(5));
  Serial.print(",");
  Serial.print(analogRead(6));
  Serial.print(",");
  Serial.println(analogRead(7));

  // Print analog inputs 1 through 7 to serial with /a(n)/ prepend
//  for (int analogChannel = 1; analogChannel < 8; analogChannel++) {
//    int sensorReading = analogRead(analogChannel);
//    Serial.print("/a");
//    Serial.print(analogChannel);
//    Serial.print("/");
//    Serial.print(sensorReading);
//    Serial.println();
//  }

  //delay(30);

}
