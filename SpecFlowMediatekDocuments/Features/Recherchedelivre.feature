
Feature: Recherchedelivre

@mytag
Scenario: Afficher les livres avec le titre  
	Given je me positionne sur l onglet livre
	When je saisis la partie du titre de livre 'les' 
	Then le nombre de livres obtenu est de 4  
	# Liste attendue : 
	# - 00007 Dans les coulisses du musée
	# - 00019 Guide Vert - Iles Canaries 
	# - 00005 Les anonymes
	# - 00021 Les déferlantes

Scenario: Afficher le livre avec un numero
	Given je me positionne sur l onglet livre
	Given je saisis le numero de livre '00021' 
	When je clique sur le bouton recherche livre
	Then le nombre de livres obtenu est de 1 
	# Liste attendue :
	# - 00021 Les déferlantes

Scenario: Afficher les livres pour un genre
	Given je me positionne sur l onglet livre
	When je sélectionne le genre 2
	Then le nombre de livres obtenu est de 5
# genre :
	# - 10001 Bande déssinée

Scenario: Afficher les livres pour un public
	Given je me positionne sur l onglet livre
	When je sélectionne le public 1
	Then le nombre de livres obtenu est de 12
# public :
	# - 00002 Adulte

Scenario: Afficher les livres pour un rayon
	Given je me positionne sur l onglet livre
	When je sélectionne le rayon 3
	Then le nombre de livres obtenu est de 1
# rayon :
	# - JN001 Jeunnesse BD
