## Green Code — Démarche éco-responsable

Ce projet intègre une réflexion sur l'éco-conception logicielle (Green Code),
visant à réduire l'empreinte environnementale de l'application.

### Principes appliqués

- **Réduction des appels réseau** : RiskService regroupe ses appels vers
  PatientService et NoteService en deux requêtes ciblées plutôt qu'en
  interrogeant des endpoints larges.

- **Algorithme sobre** : le comptage des déclencheurs dans RiskService
  concatène toutes les notes en une seule chaîne avant de chercher chaque
  terme — une seule passe par déclencheur au lieu de boucles imbriquées.

- **Réutilisation des connexions HTTP** : utilisation de `IHttpClientFactory`
  dans RiskService pour mutualiser les `HttpClient` et éviter l'épuisement
  des sockets (socket exhaustion).

- **Images Docker légères** : chaque service utilise un build multi-stage
  (`mcr.microsoft.com/dotnet/aspnet` en image finale) — seul le runtime
  est embarqué, pas le SDK de compilation.

- **Requêtes ciblées** : les endpoints ne chargent que les données
  nécessaires (ex : GET /api/note/patient/{id} retourne uniquement les
  notes du patient concerné, pas toute la collection).

### Pistes d'amélioration futures

- Mise en cache des évaluations de risque (ex : Redis) pour éviter de
  recalculer à chaque requête si les notes n'ont pas changé.
- Indexation de la collection MongoDB sur `PatientId` pour éviter les
  scans complets de collection.
- Pagination des notes pour les patients avec un historique volumineux.
- Suppression du logging verbeux en environnement de production.