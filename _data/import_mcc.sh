while read i; do curl -i -X POST -H "Content-Type: application/json" --data "$i" http://localhost:5109/api/merchant-category-codes ; done <mcc.json
