import dotenv
import os

dotenv.load_dotenv(".env")

TOKEN = os.getenv("TOKEN")
CATALOG_ID = os.getenv("CATALOG_ID")
