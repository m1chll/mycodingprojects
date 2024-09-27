# Dockerfile für den PiManager
FROM python:3.9-slim

# Setze das Arbeitsverzeichnis im Container
WORKDIR /app

# Kopiere den Manager-Code in den Container
COPY mc_manager.py /app/

# Setze den Startbefehl für den Manager
CMD ["python", "mc_manager.py"]
