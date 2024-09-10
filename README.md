# PasSec
password security app to manage internal passwords

## version 
- v1.0.0

## release date
- 09/09/2024

## pre-requisites
- docker and docker compose

## deployment guide

1. rename `.env.example` file to  `.env`
2. update `.env` variables (see more details below)
3. run the build script `run_project.sh`

## .env variables
| variable | accepted values | description |
| --- | --- | --- |
`CONFIGURATION` | `production`, `development` | system configuration 
| `web_port` | - | port to serve the website |
| `api_secret` | - | 32 character random secret key. |
| `db_name` | `passec-prod` | database name |
| `db_user` | - | database user |
| `db_pass` | - | database password must be greater than 8 character containing capital letters and digits |
| `db_root_pass` | - | root password with same requirements as `db_pass` |
| `db_port` | - | database port number |
| `connection` | - | database connection string |
| `whitelist_urls` | - | whitelist frontend sites for cors access |

## run_project.sh

`run_project.sh` script builds docker images required, initializes containers, and runs migraitons.
- it might become difficult to detect errors from the build script logs. please adjust the `run_project.sh` per need.
- commands can also be run individually and separately. 

### note
    this application is still under development.

