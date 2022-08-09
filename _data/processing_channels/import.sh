for f in *.json; do curl -X POST -H 'Content-Type: application/json' http://localhost:5109/api/processing-channels --data @$f ; done
