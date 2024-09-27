# Dockerfile für den PiManager
FROM python:3.9-slim

# Installiere Abhängigkeiten
RUN pip install --no-cache-dir socket

# Kopiere den Manager-Code in den Container
WORKDIR /app
COPY manager.py /app/manager.py

# Setze den Startbefehl für den Manager
CMD ["python", "manager.py"]
