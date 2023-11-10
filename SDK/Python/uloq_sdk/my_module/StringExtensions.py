
import json

def to_json(obj: object) -> str:
    return json.dumps(obj, default=lambda o: o.__dict__)

def from_json(json_str: str) -> object:
    return json.loads(json_str)
