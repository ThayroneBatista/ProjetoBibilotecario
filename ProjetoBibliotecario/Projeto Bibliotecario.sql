drop database BDBiblio
go
create database BDBiblio
go
use BDBiblio
go
drop table TbCategoria
go
create table TbCategoria (
CodCategoria Int Primary Key Identity (1,+1),
NomeCategoria varchar(35) not null)
go
drop table TbEditora
go
create table TbEditora (
CodEditora int Primary Key Identity (1,+1),
NomeEditora varchar(35) not null)
go
drop table TbAutor
go
create table TbAutor (
CodAutor int primary key Identity (1,+1),
NomeAutor varchar(80) not null)
go
drop table TbLivro
go
create table TbLivro (
CodLivro int primary key Identity (1,+1),
Titulo varchar(50) not null,
CodAutor int foreign key references TbAutor (CodAutor),
CodEditora int foreign key references TbEditora (CodEditora),
CodCategoria int foreign key references TbCategoria (CodCategoria),
AcompDVD bit,
Idioma int,
Observações text,
Emprestado bit,
CodUsuario int foreign key references TbUsuario (CodUsuario),
DataEmprestimo DateTime,
DataDevolução DateTime)
go
drop table TbUsuario
go
Create table TbUsuario (
CodUsuario int primary key identity (1,+1),
NomeUsuario varchar(35) not null,
SenhaUsuario varchar(12) not null,
Endereco varchar(100) ,
CodCidade int foreign key references TbCidade (CodCidade),
CodEstado int foreign key references TbEstado (CodEstado),
CEP char(9) not null,
Telefone varchar(15) not null)
go
drop table TbEstado
go
create table TbEstado (
CodEstado int Primary Key Identity (1,+1),
NomeEstado varchar(35) not null,
UFEstado char(2))
go
drop table TbCidade
go
create table TbCidade (
CodCidade int primary key Identity (1,+1),
NomeCidade varchar(35) not null,
CodEstado int foreign key references TbEstado (CodEstado))
go

create table TbIdioma (
CodIdioma int primary key Identity (1,+1),
Idioma varchar(20) not null)

select * from TbUsuario

insert into TbUsuario
values
('Thayrone','thayrone','Rua Ambar',null,null,'35162019','3189727327')


insert into TbUsuario
values
('uii002','boata123','Rua Ambar',null,null,'35162019','3189727327')

select * from TbUsuario

insert into TbAutor
values
('Thayrone Batista')

select * from TbAutor

select * from TbAutor where NomeAutor like '%yro%'


insert into TbCategoria values
('Mitologia')
select * from TbCategoria

select * from TbAutor
insert into TbAutor values
('Thayrone Batista')

select * from TbEditora
insert into TbEditora values ('SOAD')

insert into TbAutor values ('@autor')

update TbAutor set NomeAutor = 'Arthur' where NomeAutor = '@autor'

delete from TbUsuario where CodUsuario = 2

select * from TbUsuario

insert into TbAutor values ('Arthur')

select * from TbEstado where NomeEstado = 'Minas Gerais' or UFEstado = 'SP'

insert into TbEstado values ('Minas Gerais' , 'MG')

insert into TbEstado values ('Sao Paulo' , 'SP')

update TbEstado set NomeEstado = '@esstado', UFEstado = 'ES' where NomeEstado = 'Sao Paulo'

update TbEstado set NomeEstado = '@estado', UFEstado = 'uf' where NomeEstado = 'Sao Paulo'

insert into TbCategoria values ('GOT')

select * from TbCategoria

select * from TbCidade

insert into TbCidade  values ('Ipatinga',2)

Select * from tbcidade

Select * from tbestado

select * from tbusuario

select distinct tbcidade.*, tbestado.nomeestado
from tbcidade
inner join tbestado
on (tbcidade.codestado=tbestado.codestado)

select tbusuario.*, tbcidade.NomeCidade, tbestado.NomeEstado
from TbUsuario
inner join TbEstado
  on (TbUsuario.CodEstado=TbEstado.CodEstado)
inner join tbcidade
  on (tbusuario.codestado=tbcidade.codcidade)

select * from TbUsuario

insert into TbUsuario values ('Haikaiss','DMC','Iguaçu','1','1','31516521','3185484211')

select * from TbEstado

select * from tbeditora

select distinct TbCidade.CodEstado, TbEstado.NomeEstado from TbCidade
inner join TbEstado 
	on (TbCidade.CodEstado=TbEstado.CodEstado)

update TbCidade set NomeCidade = 'Gramados', CodEstado = 15 where NomeCidade = 'Gramados'

select * from tbcidade

select * from tbestado

insert into TbEstado values ('Amazonas','AM')

select distinct TbEstado.NomeEstado, TbCidade.CodEstado from TbEstado
inner join TbCidade 
	on (TbEstado.CodEstado=TbCidade.CodEstado)

select CodEstado,NomeEstado from TbEstado

select * from tbcidade

select distinct TbCidade.CodEstado, TbEstado.NomeEstado from tbcidade inner join TbEstado on (TbCidade.CodEstado=TbEstado.CodEstado)

select * from tbusuario

insert into tbusuario values ('Thayrone','thayrone','Iguaçu','4','6','35162019','3189727327')

select TbEstado.NomeEstado, TbCidade.CodEstado from TbEstado
inner join TbCidade
	on (TbEstado.CodEstado=TbCidade.CodEstado)

select C.*,E.NomeEstado from TbCidade as C
inner join TbEstado as E
	on (E.CodEstado=C.CodEstado)
where C.CodEstado = 6

select C.NomeCidade,C.CodCidade,E.NomeEstado from TbCidade as C inner join TbEstado as E on (E.CodEstado=C.CodEstado) where C.CodEstado = 6

alter table TbUsuario add constraint fk_estado foreign key (CodEstado) references TbEstado (CodEstado)

select * from TbLivro

insert into TbLivro values ('Mochileiro das galaxias','1','2','3','1','2','aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa','2','1','2001/01/05','2005/09/08')

update TbLivro set CodAutor = '4', CodEditora = '1', CodCategoria = '2' where CodLivro = 2



Select L.CodLivro,L.Titulo,A.NomeAutor,E.NomeEditora,C.NomeCategoria,L.AcompDVD,L.Idioma,L.Observações from TbLivro as L inner join TbAutor as A on (A.CodAutor = L.CodAutor) inner join TbEditora as E on (E.CodEditora = L.CodEditora) inner join TbCategoria as C on (C.CodCategoria = L.CodCategoria)

insert into TbIdioma (Idioma) values ('Portugues')
insert into TbIdioma values ('Ingles')
insert into TbIdioma values ('Japones')
insert into TbIdioma values ('Russo')
insert into TbIdioma values ('Espanhol')
insert into TbIdioma values ('Mandarim')
insert into TbIdioma values ('Mexicano')

select * from TbIdioma

select * from TbLivro

select CodLivro , Titulo , Emprestado from TbLivro where Emprestado = 1

insert into TbLivro values ('Livro 1','1','1','1','1','1','1','1','1','2001/01/05','2005/09/08')
insert into TbLivro values ('Livro 2','2','2','2','2','2','2','0','2','2001/01/05','2005/09/08')
insert into TbLivro values ('Livro 3','3','3','3','3','3','3','1','3','2001/01/05','2005/09/08')
