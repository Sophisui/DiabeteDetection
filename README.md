## Green Code — Démarche éco-responsable

Ce projet intègre une réflexion sur la façon de réduire l'impact environnemental du logiciel : consommer moins de ressources (calcul, réseau, mémoire) pour le même résultat.

### Principes appliqués

- **Réduction des appels réseau** : RiskService ne demande que ce dont il a besoin — les informations du patient d'un côté, ses notes de l'autre. Pas de données superflues chargées.

- **Algorithme sobre** : pour chercher les termes médicaux dans les notes, toutes les notes sont d'abord regroupées en un seul texte, puis chaque terme est cherché en une seule lecture. Cela évite de relire les mêmes notes plusieurs fois.

- **Réutilisation des connexions HTTP** : les connexions réseau entre services ne sont pas recréées à chaque requête — elles sont mutualisées. Cela évite un gaspillage de ressources à chaque appel.

- **Images Docker légères** : les images Docker utilisées en production ne contiennent que ce qui est strictement nécessaire pour faire tourner l'application — les outils de développement ne sont pas inclus, ce qui réduit la taille et la consommation au démarrage.

- **Requêtes ciblées** : chaque service ne récupère que les données dont il a besoin. Par exemple, on ne charge pas toutes les notes de tous les patients pour en afficher une seule.

### Pistes d'amélioration futures

- Mémoriser les résultats déjà calculés : si les notes d'un patient n'ont pas changé, inutile de recalculer son niveau de risque à chaque consultation — on pourrait stocker temporairement le dernier résultat.
- Accélérer les recherches dans la base de notes : en ajoutant un repère sur l'identifiant du patient dans MongoDB, les recherches seraient bien plus rapides sur un grand volume de données.
- Afficher les notes par pages : pour un patient avec un historique très long, charger toutes les notes d'un coup n'est pas optimal. Les afficher par tranches serait plus sobre.
- Réduire les journaux en production : en développement, on enregistre beaucoup d'informations pour déboguer. En production, ces journaux détaillés sont inutiles et consomment des ressources.
