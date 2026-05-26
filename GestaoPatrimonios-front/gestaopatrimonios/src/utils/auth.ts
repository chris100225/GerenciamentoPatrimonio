import secureLocalStorage from "react-secure-storage";

// não é async por não estarmos fazendo uma requisiçõa na api
export function verificarAutenticacao() {
    // o parametro do getItem precisa ser o mesmo nome do item que foi salvo no localStorage
    const token = secureLocalStorage.getItem("Token");
    return !!token; // se o token existir, retorna true, se não retorna false
}