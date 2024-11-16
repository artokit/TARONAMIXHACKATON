import uvicorn
from routers import neural_router
from fastapi import FastAPI

app = FastAPI()

app.include_router(neural_router.router)

if __name__ == '__main__':
    uvicorn.run(app, host="0.0.0.0", port=8001)
