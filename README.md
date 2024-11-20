## CrossPlatform
#### Варіант 49
## Команди для збірки, запуску, тестування лабораторної роботи
```bash
dotnet build Build.proj -p:Solution=Lab1 -t:Build
```
```bash
dotnet build Build.proj -p:Solution=Lab1 -t:Run
```
```bash
dotnet build Build.proj -p:Solution=Lab1 -t:Test
```
## Команди для Лабораторної 3
1. Створюємо директорію для локального репозиторію:
```bash
mkdir LabRepo
```
2. Створюємо Nuget пакет бібліотеки класів:
```bash
cd Lab3Library
```
```bash
dotnet pack --output "../LabRepo"
```
3. Додаємо локальний репозиторій до нашого проекту:
```bash
cd ../Lab3
```
```bash
dotnet nuget add source "../LabRepo" --name LabRepo
```
4. Додаємо пакет до проекту:
```bash
dotnet add package VKomissarovLab3 --version 1.0.0 --source "../LabRepo"
```
## Команди для розгортання Лабораторної 5
```bash
vagrant up lab5
```
Для того, щоб відкрити перейдіть: http://localhost:3000
