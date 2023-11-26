#!/bin/sh

echo "Replacing the backend/api in appsettings.json with the ServerUrl environment variable..."
sed -i "/ServerUrl/c\   "\"ServerUrl"\" : "\"$ServerUrl"\"," appsettings.json

echo "Replacing the listen port in default.conf with the PORT environment variable..."
sed -i "s/listen.*/listen $PORT;/" /etc/nginx/conf.d/default.conf
