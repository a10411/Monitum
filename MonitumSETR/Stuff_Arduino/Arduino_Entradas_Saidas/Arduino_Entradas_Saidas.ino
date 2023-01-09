// Definição dos pins aos quais estão conectados os sensores, botões, etc e ainda a threshold distance (distância máxima de captação de objeto nos sensores ultrassonicos)
#define TRIGGER_PIN_1 5  // Trigger pin do sensor ultrassonico 1
#define ECHO_PIN_1 6     // Echo pin do sensor ultrassonico 1
#define TRIGGER_PIN_2 3  // Trigger pin do sensor ultrassonico 2
#define ECHO_PIN_2 4     // Echo pin do sensor ultrassonico 2
#define THRESHOLD_DISTANCE 5 // Distância máxima de captação de objeto
#define BUTTON_PIN 2 // Pin do botão do interrupt (tem de ser um dos pins dedicado aos interrupts)

const int PINO_SENSOR_RUIDO = A0; // Pin do sensor de ruído (entrada analógica 0)

int count = 0; // Contador de pessoas no espaço
int apiRequestCounter = 1; // Contador que auxilia o envio de requests à API
int countRuido = 0; // Contador de deteções de ruído

bool sensor1Triggered = false; // Deteta se o sensor 1 ultrassonico foi triggered (ativado por objeto próximo)
bool sensor2Triggered = false; // Deteta se o sensor 2 ultrassonico foi triggered (ativado por objeto próximo)
unsigned long sensor1Millis; // Variável utilizada para auxílio no cálculo do tempo de passagem entre sensores (a passagem entre estes deve ser feita entre um determinado número de segundos, para ter mais precisão na contagem)
unsigned long sensor2Millis; // Variável utilizada para auxílio no cálculo do tempo de passagem entre sensores
unsigned long apiMillis; // Variável utilizada para auxiliar no envio de infos para a API de 5 em 5 minutos



void setup() {
  
  // Inicializar os pins
  pinMode(TRIGGER_PIN_1, OUTPUT);
  pinMode(ECHO_PIN_1, INPUT);
  pinMode(TRIGGER_PIN_2, OUTPUT);
  pinMode(ECHO_PIN_2, INPUT);
  pinMode(BUTTON_PIN, INPUT_PULLUP);
  pinMode(PINO_SENSOR_RUIDO, INPUT);

  // Inicializar comunicação serial
  Serial.begin(9600);

  // Associar o pin ao interrupt (myISR)
  attachInterrupt(digitalPinToInterrupt(BUTTON_PIN), myISR, FALLING); // "FALLING" -> ativa quando botão é pressionado
}

void loop() {
  apiMillis = millis(); // Tempo de execução atual (está sempre a dar refresh)

  // OCUPACAO CALCULO

  // Medir distância a cada sensor
  int distance1 = measureDistance(TRIGGER_PIN_1, ECHO_PIN_1);
  int distance2 = measureDistance(TRIGGER_PIN_2, ECHO_PIN_2);

  //Serial.println(distance1);
  //Serial.println(distance2);
  //delay(100);

  // Se a distancia ao sensor 1 for inferior à threshold
  if (distance1 < THRESHOLD_DISTANCE) {
    if (sensor2Triggered == true){ // caso sensor 2 tenha sido triggered (neste caso vai detetar uma saída)
      if (sensor2Millis >= millis() - 1000 && count > 0){ // o sensor 2 tem de ter sido triggered há menos de um segundo para funcionar
        count--;
        Serial.println("Saiu");
        Serial.println(count);
      }
    } else {
      sensor1Triggered = true; // caso o sensor 2 não tenha sido triggered, passamos a informar que o sensor1 foi triggered
      sensor1Millis = millis(); // registamos o tempo em que o sensor 1 foi triggered
    }
    sensor2Triggered = false; // colocamos a variável sensor2triggered de volta a false
  } 
  if (distance2 < THRESHOLD_DISTANCE){ // desta vez exatamente a mesma coisa, mas com o sensor ultrassonico 2
    if (sensor1Triggered == true){
      if (sensor1Millis >= millis() - 1000){
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
  //delay(100);
  
  if (analogRead(PINO_SENSOR_RUIDO) < 900){ // Após afinarmos o sensor via potenciómetro, caso ele baixe de 900 quer dizer que detetou algum ruído
    //Serial.println("Algum ruido!");
    countRuido++; // incrementa em uma unidade o contador de ruído
  } else if (analogRead(PINO_SENSOR_RUIDO) < 700){ // Após afinarmos o sensor via potenciómetro, caso baixe de 700 (a leitura) quer dizer que detetou muito ruído
    //Serial.println("Muito ruido!");
    countRuido+=5; // incerementa em cinco unidades o contador de ruído
  }

  if (apiMillis/1000 >= 300 * apiRequestCounter){ // 300 = 5 minutos
    // API STUFF
    sendStuffToAPI();
  }

  // classificação das salas quanto ao ruído
  // +50 algum ruido
  // +200 ruidosa
  // +500 muito ruidosa
  
   
  
}

// Função de medição de distância ao sensor ultrassónico
int measureDistance(int triggerPin, int echoPin) {
  long duration, distance;

  // Enviar um pulso para o triggerPin
  digitalWrite(triggerPin, LOW);
  delayMicroseconds(2);
  digitalWrite(triggerPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(triggerPin, LOW);

  // Medir a distância ao EchoPin
  duration = pulseIn(echoPin, HIGH);

  // Calcular a distância com base na duração do pulse
  distance = duration * 0.034 / 2;

  return distance;
}

// Função de interrupt
void myISR() {
  sendStuffToAPI(); // chama função de enviar dados para API
}

// Função de "envio" de dados para API
void sendStuffToAPI(){
  // API STUFF
  Serial.println("Informacao enviada para a API (simulacao):");
  Serial.print("Numero de pessoas na sala: ");
  Serial.println(count);
  Serial.print("Classificacao ruido na sala: ");
  if (countRuido > 500){  
    Serial.println("Muito Ruidosa");
  }
    
  else if (countRuido > 200){
    Serial.println("Ruidosa");
  }
  else {
    Serial.println("Pouco ruidosa");
  }
    
  apiRequestCounter++;
  countRuido = 0;
}

