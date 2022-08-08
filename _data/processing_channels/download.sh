# Download all processing channels present in sbox_ids.csv
# In order to run this script replace [REDACTED] with the actual authorisation header for the specific environment

while read i; do wget --no-check-certificate --quiet -O $i.json --header 'Authorization: [REDACTED]' http://gwc.sbox.internal/admin-api/processing-channels/$i  ; done < sbox_ids.csv
