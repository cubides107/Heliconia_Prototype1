import { secret } from "@/assets/enums/Secret.js"

var jwt = require('jsonwebtoken');

export function descifrarToken(token) {

    var data = jwt.verify(token, secret.claveSecreta, function (err, decoded) {
        if (err) {
            return err.message;
        }
        else {
            return decoded
        }
    })

    return data;

}