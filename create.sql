SET foreign_key_checks = 0;

drop table if exists user_log;


 
 create table user_log(
	connection_code bigint not null,
    user_id int,
    user_name varchar(30),
    connection_time date not null,
    
    primary key (connection_code),
    
    CHECK(connection_code >0)
);
 
 
 
 create table comptadors(

	taula varchar(30) not null,
   comptador bigint not null,    
    primary key(taula),
    CHECK(length(taula)>0),
    CHECK(comptador >=0)

); 


create table soci(

  id int not null,
  nif varchar(9) not null unique,
  nom varchar(30) not null,
  data_alta timestamp not null,
  cognom1 varchar(30) not null,
  cognom2 varchar(30),
  passwordHash varchar(32) not null unique,  
  foto longblob,
  estadistica_modalitat int  not null, 
  primary key(id),  
  CHECK(id >0),
  CHECK(length(nif)=9),
  CHECK(length(nom)>0),
  CHECK(length(cognom1)>0) , 
  CHECK(length(passwordHash)>0)
);


create table partida(
  id int not null,
  carambolesA int ,
  carambolesB int ,  
  data_partida timestamp not null,
  inscrit_a int not null,
  inscrit_b int not null,
  num_entrades int ,  
  estat_partida varchar(1) not null,
  guanyador varchar (1) ,
  motiu_victoria varchar (20),  
  
  primary key(id),  
  CHECK(id>0),
  CHECK(import_cobert >0),
  CHECK(num_perit>0),
  CHECK(length(estat_partida)>0),  
  FOREIGN KEY (inscrit_a )REFERENCES soci(id),
  FOREIGN KEY (inscrit_b) REFERENCES soci(id)
  
);

create table modalitat(
  id  int not null ,  
  desc_modalitat varchar(30) not null,    
  CHECK(length(id)>0),
  CHECK(length(desc_modalitat)>0),  
  primary key(id)  
  
);


create table torneo(
    id int not null,    
    nom varchar(30) not null,    
    data_inici timestamp  not null,
    id_modalitat int not null,
    id_inscrit int , 
    preinscricion int not null, 
    primary key (id),     
    CHECK(id >0), 
    CHECK(length(nom)>0), 
    check (preinscricion >0  or preinscricion<1),
    FOREIGN KEY (id_modalitat) REFERENCES modalitat(id)
    
    
);

create table grup(
  id int not null,
  id_torneig int not null,
  descripcio varchar(100) not null,
  caramboles_vistoria int ,
  limit_entrades int , 
  mismo_grupo int not null,
 
  id_partida int ,
  primary key(id,id_torneig,mismo_grupo),  
  CHECK(id >0),
  CHECK(id_torneig >0), 
  CHECK(length(descripcio)>0),  
  FOREIGN KEY (id_torneig) REFERENCES torneo(id),
  FOREIGN KEY (id_partida) REFERENCES partida(id)
);


create table inscrito(

  id_soci int not null,
  id_grup int not null,
  id_torneig int not null,  
  data_inscrp timestamp ,  
  
  CHECK(length(id_soci)>0),  
  primary key(id_soci,id_grup,id_torneig),  
  FOREIGN KEY (id_soci) REFERENCES soci (id),
  FOREIGN KEY (id_torneig) REFERENCES torneo (id),
  FOREIGN KEY (id_grup) REFERENCES grup (id)
  
);




--ALTER TABLE grup ADD CONSTRAINT fk_frup FOREIGN KEY (num_inscrit) REFERENCES inscrit(id_soci);



create table estadistica_modalitat(
--pueda que sea obligatorio 
  coeficient_base int ,  
  total_carambolas_temporada_actual int,
  total_entrades_temporada_actual int,
  id_soci int not null,
  id_modalitat int not null,
  CHECK(length(id_soci)>0),
  CHECK(length(id_modalitat)>0),  
  primary key(id_soci,id_modalitat),
  
  FOREIGN KEY (id_soci) REFERENCES soci (id),
  FOREIGN KEY (id_modalitat) REFERENCES modalitat (id)
);

