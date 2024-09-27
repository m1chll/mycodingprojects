# Dockerfile für den Worker
FROM python:3.9-slim

# Installiere Abhängigkeiten
RUN pip install --no-cache-dir socket

# Kopiere den Worker-Code in den Container
WORKDIR /app
COPY mc_worker.py /app/mc_worker.py

# Setze den Startbefehl für den Worker
CMD ["python", "mc_worker.py"]
