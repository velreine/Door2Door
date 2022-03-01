# Deployment

API'en forventer at miljøvariablen "connection_string" er sat.

**Linux** 

```bash
export connectionString='postgres://user:pw@somehost/somedb'
```

**Windows**:

```bash
SET connectionString="postgres://user:pw@somehost/somedb"
```

**Docker**:

```bash
docker run --name -e connectionString="postgres://user:pw@somehost/somedb" somecontainer somecontainer:latest
```



Projektet kommer med en **docker-compose.yml** fil som er en skabelon til Docker Compose.

Hvis hele projektet skal startes med Docker Compose kan dette opnås sådan her:

```bash
cd <projectDir>
docker-compose up --detach
```

**--detach** flaget er optionelt og kører bare containers i baggrunden.



**NB**: Husk at ændre adgangskode-miljøvariabler i docker-compose.yml.
