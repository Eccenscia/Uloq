
import aiohttp
from typing import TypeVar, Generic
from ..models import ConnectionModel, OutcomeObject

T = TypeVar('T')

class HttpConnector(Generic[T]):
    def __init__(self, connection_model: ConnectionModel):
        self._connection_model = connection_model
    
    async def post_async(self, endpoint: str, payload: str) -> OutcomeObject[T]:
        headers = {
            'Authorization': f"Bearer {self._connection_model.api_key}:{self._connection_model.api_secret}",
            'Content-Type': 'application/json'
        }
        
        async with aiohttp.ClientSession() as session:
            async with session.post(f"{self._connection_model.base_url}/{endpoint}", data=payload, headers=headers) as response:
                if response.status == 200:
                    data = await response.json()
                    return OutcomeObject(success=True, result=data)
                else:
                    message = await response.text()
                    return OutcomeObject(success=False, message=message)
