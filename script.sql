drop table tbcliente;
create database LoginApp;

use LoginApp;

create table tbCliente(
Id int auto_increment primary key ,
Nome varchar(50),
DataNasc date,
CPF decimal(11,0),
Telefone decimal (11,0),
Email varchar(50),
Sexo varchar(10),
Senha varchar(1000),
Situacao varchar(20)
);

-- Colaborador é um operador do sistema
create table tbColaborador(
Id int auto_increment primary key ,
Nome varchar(50),
Email varchar(50),
Senha varchar(1000),
TipoColaborador varchar(50)
);

describe tbCliente;