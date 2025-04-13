# Dr VAN Topor Crazy Run - README

## 1. Comment jouer ?

### Principe de gameplay

Dr VAN Topor Crazy Run est un infite runner où le joueur contrôle un docteur infecter par des maladie, pour se soigner il doit collecter des gélule de différents type.
L'objectif est de faire le meilleur score en survivant le plus longtemps.

Commandes

> Le joueur peut se déplacer grâce aux touches directionnelles ou le joystick d'une manette.

Comment gagner ?

> Pas de condition de victoire le but étant de faire le meilleur score.

Comment perdre ?

> L'une des jaunes de couleur est à 0.

## 2. Le code source

### Structure

Le projet suit une architecture orientée objet.

```
Assets/
├── Audio/                 # Contient les ressources audio
│   ├── Musics/
│   └── Sounds/
├── Graphics/              # Contient les ressources graphiques
│   ├── Materials/
│   ├── Models/
│   │   └── Animations/
│   └── Textures/
├── Prefabs/               # Contient les préfabriqués
│   ├── Grounds/
│   └── Gelule/
├── Scenes/                # Contient les scènes
├── Scripts/               # Contient les scripts
├── UI/
└── Save and Load/
```



### Design pattern et techniques utilisés :

- Singleton : GameManager
- Coroutine : GeluleSpawner
- Game Loop : Awake, Start, Update, Exit
- Events : OnTriggerExit, OnColliderEnter, OnClick, etc..
- UnityEvent : OnInitLife, OnChangeLife,...
- Algorithme de la roulette : Permet de générer les Gelule en fonction de la barre de vie et d’un poids

### Génération procédurale du terrain et des objets
- Le sol générer à partir d’un préfab Ground et d’un empty Spawner avec une collision box qui permet la création d’un nouveau Ground à chaque fois l’objet sort de celle-ci.
- Les Gelule générer aléatoirement à partir de 5 points de spawn (3 au départ, 4 au bout de 60s puis 5 au bout de 3 min).
- Un Scroller permet de faire avancer le tout.
- Un empty Destroyer permet de supprimer les éléments passer derrière le joueur hors de l’écran.


