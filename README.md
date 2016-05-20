# MHTemperature
Begonnen als kleine Bibliothek um die Wassertemperatur des Freibad Marienhöhe für eine Mac OS X App abzurufen,
gibt es nun auch einen REST Web Service, der die Daten automatisch aggregiert und dynamisch zur Verfügung stellt.

Derzeit läuft die Anwendung auf einem Windows Server 2012 R2 und stellt die Daten für eine Android App bereit.

![Android App Screenshot](https://github.com/LinuxDoku/MHTemperature/raw/master/Android-App.png "Android App")

## System Anforderungen

- .NET 4.5.2 oder aktueller als Runtime
- PostgreSQL als Datenspeicher

## Technisches

- Html Parser: HtmlAgilityPack
- Windows Service: TopShelf
- Orm: Entity Framework
- Api: NancyFx
- Datenbank: PostgreSQL
- Tests: NUnit, Moq