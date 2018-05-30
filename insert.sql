






insert into soci values(11025,"12345678F","Alex",date'2016-12-03',"Cano","Calaf","0DE24EBFD92565A643A5C83D57D24699",null,2);
insert into soci values(20025,"11223344A","Albert",date'2018-10-09',"Rata","Perez","0e29be28f22f45dc8fbb973e81645dd7",null,3);
insert into soci values(37852,"55667788B","Oriol",date'2016-12-1',"Bermudez","Rovira","d07f55040ac2236bec1df6b8a7da51b3",null,2);
insert into soci values(43450,"222334455C","Carlos",date'2015-01-11',"Mingoranze","Jene","fcb1cb75483e6bc683fa92f7c4dd9611",null,2);
insert into soci values(50225,"99887766H","Cristian",date'2017-12-12',"Lopez",null,"3cd5d210acb9a1b81c04150ca305d662",null,1);

insert into partida values(1,20,21,null,11025,37852,2,'P',null,null);
insert into partida values(2,50,25,null,37852,20025,1,'J','A',2);
insert into partida values(3,60,45,null,37852,50225,1,'J','A',3);

insert into modalitat values(1,'lliure');
insert into modalitat values(2,'a 1 banda');
insert into modalitat values(3,'a 3 bandas');



insert into torneo values (1,"city",date'2018-09-25',1,null,0);
insert into torneo values (2,"la bola negra",date'2018-12-27',2,null,0);
insert into torneo values (3,"round3bandas",null,1,null,0);
insert into comptadors values("TORNEO",5);
insert into comptadors values("GRUP",4);



--insert into inscrit values (11025,1,1,null);
--insert into inscrit values (37852,2,2,null);
--insert into inscrit values (50225,3,3,null);
--insert into inscrit values (20025,1,2,null);
 










--insert into grup values(1,1,"grupo master con campeonatos ganados en el ultimo año",60,10,null,1);
--insert into grup values(2,2,"grupo senior con campeonatos ganados en el ultimo año",30,12,null,2);
--insert into grup values(3,3,"grupo master sin participacones en otros torneos",120,50,null,3);


--UPDATE grup SET num_inscrit=11025 where id=1;
--UPDATE grup SET num_inscrit=37852 where id=2;
--UPDATE grup SET num_inscrit=20025 where id=3;

