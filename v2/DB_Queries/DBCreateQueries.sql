CREATE TABLE Gestor (id_gestor int IDENTITY NOT NULL, email varchar(255) NOT NULL, password_hash varchar(255) NOT NULL, password_salt varchar(255) NOT NULL, PRIMARY KEY (id_gestor));
CREATE TABLE Estabelecimento_Gestor (id_estabelecimento int NOT NULL, id_gestor int NOT NULL, PRIMARY KEY (id_estabelecimento, id_gestor));
CREATE TABLE Estabelecimento (id_estabelecimento int IDENTITY NOT NULL, nome varchar(255) NOT NULL, morada varchar(255) NULL, PRIMARY KEY (id_estabelecimento));
CREATE TABLE Horario_Sala (id_horario int IDENTITY NOT NULL, id_sala int NOT NULL, dia_semana varchar(255) NOT NULL, hora_entrada time(7) NOT NULL, hora_saida time(7) NOT NULL, PRIMARY KEY (id_horario));
CREATE TABLE Log_Metrica (id_log int IDENTITY NOT NULL, id_sala int NOT NULL, id_metrica int NOT NULL, valor_metrica int NOT NULL, data_hora time(7) NOT NULL, PRIMARY KEY (id_log));
CREATE TABLE Metrica (id_metrica int IDENTITY NOT NULL, nome varchar(255) NOT NULL, medida varchar(255) NULL, PRIMARY KEY (id_metrica));
CREATE TABLE Sala (id_sala int IDENTITY NOT NULL, id_estabelecimento int NOT NULL, id_estado int NOT NULL, PRIMARY KEY (id_sala));
CREATE TABLE Estado (id_estado int IDENTITY NOT NULL, estado varchar(255) NOT NULL, PRIMARY KEY (id_estado));
CREATE TABLE Comunicado (id_comunicado int IDENTITY NOT NULL, id_sala int NOT NULL, titulo varchar(255) NOT NULL, corpo varchar(255) NOT NULL, data_hora time(7) NOT NULL, PRIMARY KEY (id_comunicado));
CREATE TABLE Administrador (id_administrador int IDENTITY NOT NULL, email varchar(255) NOT NULL, password_hash varchar(255) NOT NULL, password_salt varchar(255) NOT NULL, PRIMARY KEY (id_administrador));
ALTER TABLE Estabelecimento_Gestor ADD CONSTRAINT FKEstabeleci532222 FOREIGN KEY (id_estabelecimento) REFERENCES Estabelecimento (id_estabelecimento);
ALTER TABLE Log_Metrica ADD CONSTRAINT FKLog_Metric853999 FOREIGN KEY (id_metrica) REFERENCES Metrica (id_metrica);
ALTER TABLE Horario_Sala ADD CONSTRAINT FKHorario_Sa576410 FOREIGN KEY (id_sala) REFERENCES Sala (id_sala);
ALTER TABLE Sala ADD CONSTRAINT FKSala746893 FOREIGN KEY (id_estabelecimento) REFERENCES Estabelecimento (id_estabelecimento);
ALTER TABLE Log_Metrica ADD CONSTRAINT FKLog_Metric834239 FOREIGN KEY (id_sala) REFERENCES Sala (id_sala);
ALTER TABLE Estabelecimento_Gestor ADD CONSTRAINT FKEstabeleci479765 FOREIGN KEY (id_gestor) REFERENCES Gestor (id_gestor);
ALTER TABLE Sala ADD CONSTRAINT FKSala218687 FOREIGN KEY (id_estado) REFERENCES Estado (id_estado);
ALTER TABLE Comunicado ADD CONSTRAINT FKComunicado409509 FOREIGN KEY (id_sala) REFERENCES Sala (id_sala);
