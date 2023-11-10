from setuptools import setup, find_packages

setup(
    name="uloq_sdk",
    version="1",
    packages=find_packages(),
    install_requires=[
        "aiohttp",
        # other dependencies
    ],
    author="Eccenscia",
    author_email="technical@eccenscia.com",
    description="Uloq SDK",
    license="MIT",
    keywords="authentication and authorization uloq",
    url="https://uloq.io",  # project home page
)
