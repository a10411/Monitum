#define TRIGGER_PIN_1 4  // Trigger pin for ultrasonic sensor 1
#define ECHO_PIN_1 5     // Echo pin for ultrasonic sensor 1
#define TRIGGER_PIN_2 2  // Trigger pin for ultrasonic sensor 2
#define ECHO_PIN_2 3     // Echo pin for ultrasonic sensor 2
#define THRESHOLD_DISTANCE 5 // Threshold distance for determining occupancy

const int PINO_SENSOR_RUIDO = A0;

int count = 0; // Counter for number of people in the space
int apiRequestCounter = 1; // Counter for API Requests
int countRuido = 0;

bool sensor1Triggered = false; // Flag to track if sensor 1 has been triggered
bool sensor2Triggered = false;
unsigned long sensor1Millis;
unsigned long sensor2Millis;
unsigned long apiMillis;

void setup() {
  
  // Initialize the trigger and echo pins as output and input, respectively
  pinMode(TRIGGER_PIN_1, OUTPUT);
  pinMode(ECHO_PIN_1, INPUT);
  pinMode(TRIGGER_PIN_2, OUTPUT);
  pinMode(ECHO_PIN_2, INPUT);

  pinMode(PINO_SENSOR_RUIDO, INPUT);

  // Inicializar comunicação serial
  Serial.begin(9600);
}

void loop() {
  apiMillis = millis();

  // OCUPACAO
  
  // Continuously measure the distance from the sensors
  int distance1 = measureDistance(TRIGGER_PIN_1, ECHO_PIN_1);
  int distance2 = measureDistance(TRIGGER_PIN_2, ECHO_PIN_2);

  //Serial.println(distance1);
  //Serial.println(distance2);
  //delay(100);

  // Update the counter based on the distance measurements and sensor order
  if (distance1 < THRESHOLD_DISTANCE) {
    if (sensor2Triggered == true){
      if (sensor2Millis >= millis() - 500 && count > 0 && sensor2Triggered == true){
        count--;
        Serial.println("Saiu");
        Serial.println(count);
      }
    } else {
      sensor1Triggered = true;

      sensor1Millis = millis();
    }
    sensor2Triggered = false;
  } 
  if (distance2 < THRESHOLD_DISTANCE){
    if (sensor1Triggered == true){
      if (sensor1Millis >= millis() - 500 && sensor1Triggered == true){
        count++;
        Serial.println("Entrou");
        Serial.println(count);
      }
    } else {
      sensor2Triggered = true;
      sensor2Millis = millis();
    }
    sensor1Triggered = false;
  }

  // SOM

  //Serial.println(analogRead(PINO_SENSOR_RUIDO)); // Le as informacoes obtidas do sensor
  delay(100);
  
  if (analogRead(PINO_SENSOR_RUIDO) < 900){
    countRuido++;
    //Serial.println("Algum ruido!");
  } else if (analogRead(PINO_SENSOR_RUIDO) < 700){
    //Serial.println("Muito ruido!");
    countRuido+=5;
  }

  if (apiMillis/1000 >= 300 * apiRequestCounter){ // 300 = 5 minutos
    // API STUFF
    Serial.println("API");
    Serial.println(count);
    Serial.println(countRuido);
    apiRequestCounter++;
  }

  // +50 algum ruido
  // +200 ruidosa
  // +500 muito ruidosa
  
   
  
}

// Function to measure the distance from an ultrasonic sensor
int measureDistance(int triggerPin, int echoPin) {
  long duration, distance;

  // Send a pulse to the trigger pin
  digitalWrite(triggerPin, LOW);
  delayMicroseconds(2);
  digitalWrite(triggerPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(triggerPin, LOW);

  // Measure the duration of the pulse on the echo pin
  duration = pulseIn(echoPin, HIGH);

  // Calculate the distance based on the duration of the pulse
  distance = duration * 0.034 / 2;

  return distance;
}
