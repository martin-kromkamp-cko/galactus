for f in *.json; do curl -X POST -H 'Content-Type: application/json' https://localhost:7209/api/processing-channels --data @$f ; done