Feature: Estoria1
	Para que eu consiga democraticamente levar meus colegas a comer onde eu gosto
	Eu como profissional faminto 
	Quero votar no meu restaurante favorito 

Background:
    Given um usuário
	And uma eleição
	And um restaurante "Palatus"
	And um restaurante "32"

Scenario: Usuário votar
	Given eu não tenha votado
	When eu votar no restaurante "Palatus"
	Then vai aparecer nos resultados somente um voto

Scenario: Um profissional só pode votar em um restaurante por dia
	Given eu tenha votado no restaurante "Palatus"
	When eu votar no restaurante "32"
	Then vai aparecer a mensagem de erro "Só é possível votar em um restaurante por dia"

Scenario: Usuário não pode votar após a data e horário limite
	Given eleição tenha sido encerrada
	When eu votar no restaurante "32"
	Then vai aparecer a mensagem de erro "Eleição já foi encerrada"