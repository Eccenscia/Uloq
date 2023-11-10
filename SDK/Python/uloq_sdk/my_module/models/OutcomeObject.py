
from typing import TypeVar, Generic, Optional

T = TypeVar('T')

class OutcomeObject(Generic[T]):
    def __init__(self, success: bool, result: Optional[T] = None, message: Optional[str] = None):
        self.success = success
        self.result = result
        self.message = message
