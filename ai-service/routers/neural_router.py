from fastapi import APIRouter
from services import neural_service

router = APIRouter()


@router.get("/softs")
async def get_soft_compare(tags1: str, tags2: str):
    res = neural_service.get_soft(tags1, tags2)
    return {
        "name": res[0].capitalize(),
        "softs_percent": res[1]
    }


@router.post("/hards")
async def get_hards_compare(tags1: list[str], tags2: list[str]):
    r = neural_service.get_hard(tags1, tags2)
    return {
        "percent": r * 100
    }


@router.get("/magic_compare")
async def get_magic_compare(first_card_name: str, second_card_name: str):
    r = neural_service.get_magic_compare(first_card_name, second_card_name)
    return r
