import { api } from "./api";

type ListaLocal = {
    localizacaoID: string;
    nomeLocal: string;
    areaID: string;
    responsavel: string;
}

export async function listarLocais() {
    try {
        //requisitando API
        const response = await api.get("Localizacao");

        //formatando os dados para o formato da aplicação
        const locais = response.data.map((local: ListaLocal) => ({
            ...local
        }));
        console.log(response.data);
        return locais;
    } catch (error: any) {
        console.log(error.message);
        return [];
    }
}