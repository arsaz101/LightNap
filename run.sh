#!/bin/bash
# Start backend
cd src/LightNap.WebApi
nohup dotnet run &
BACKEND_PID=$!
cd ../../lightnap-ng
npm install
nohup npx ng serve &
FRONTEND_PID=$!
echo "Backend PID: $BACKEND_PID"
echo "Frontend PID: $FRONTEND_PID"
wait $BACKEND_PID $FRONTEND_PID 