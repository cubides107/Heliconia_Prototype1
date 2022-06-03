export function obtenerItemLocal(key){
    const token =  localStorage.getItem(key);
    return token;
}

export function crearItemLocal(key, value){
    localStorage.setItem(key, value);
}

export function eliminarItemLocal(key){
    localStorage.removeItem(key);
}

export function existeItemLocal(key){
    if(localStorage.getItem(key) == null || localStorage.getItem(key) === ''){
        return false;
    }
    else{
        return true;
    }
}