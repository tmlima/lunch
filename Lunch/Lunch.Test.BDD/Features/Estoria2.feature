Feature: Estoria2
	Para que não precise ouvir reclamações infinitas! 
	Eu como facilitador do processo de votação 
	Quero que um restaurante não possa ser repetido durante a semana 

Scenario: O mesmo restaurante não pode ser escolhido mais de uma vez durante a semana
	Given o restaurante "Palatus" já tenha sido eleito na semana atual
	When o restaurante "Palatus" for eleito
	Then o segundo restaurante mais votado será eleito