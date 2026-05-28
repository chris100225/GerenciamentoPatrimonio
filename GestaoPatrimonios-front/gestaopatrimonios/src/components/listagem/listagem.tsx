import { useEffect, useState } from "react";
import CardLocal from "../card-local/card-local";
import styles from "./listagem.module.css";
import { listarLocais } from "@/pages/api/localService";
import { verificarAutenticacao } from "@/utils/auth";


type ListaProps = {
    page?: string;
};

interface Localizacao {
    localizacaoID: string;
    nomeLocal: string;
    areaID: string;
    responsavel: string;
}

const Listagem = ({ page }: ListaProps) => {

    const [locais, setLocais] = useState<Localizacao[]>([]);
    const [estaAutenticado, setEstaAutenticado] = useState(false);

    async function listarLocalizacao() {
        try {
            const lista = await listarLocais();
            setLocais(lista);
        } catch (error: any) {
            console.log(error.message);
        }
    }


    useEffect(() => {
        setEstaAutenticado(verificarAutenticacao());
        listarLocalizacao();
    }, [])

    return (
        <>

            {page === "locais" && (
                <section className={`${styles.table_section} ${styles.layout_guide}`} aria-label="Lista de ambientes">
                    <table className={styles.environment_table}>
                        <thead>
                            <tr>
                                <th>Nome</th>
                                <th>Área</th>
                                <th>Responsável</th>
                            </tr>
                        </thead>
                    </table>
                    {locais.length > 0 ? locais.map((local) => (
                        <CardLocal
                            key={local.localizacaoID}
                            nomeLocal={local.nomeLocal}
                            area={local.areaID}
                            responsavel={local.responsavel}
                        />
                    )) : (
                        <p>Nenhum local encontrado.</p>
                    )}
                </section>
            )}

            <nav className={styles.pagination} aria-label="Paginação">
                <button type="button" className={styles.pagination_button} aria-label="Página anterior">
                    <i className="fa-solid fa-angle-left"></i>
                </button>
                <a href="#" className={`${styles.pagination_link} ${styles.current}`} aria-current="page">
                    1
                </a>
                <a href="#" className={styles.pagination_link}>
                    2
                </a>
                <a href="#" className={styles.pagination_link}>
                    3
                </a>
                <button type="button" className={styles.pagination_button} aria-label="Próxima página">
                    <i className="fa-solid fa-angle-right"></i>
                </button>
            </nav>

        </>
    );
}
export default Listagem;