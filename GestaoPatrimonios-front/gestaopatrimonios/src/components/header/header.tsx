import { useEffect, useState } from "react";
import styles from "./header.module.css"
import { jwtDecode } from "jwt-decode";
import secureLocalStorage from "react-secure-storage";


type Usuario = {
    id: string;
    nome: string;
    email: string;
    cargo: string;
    nif: string;
}
const Header = () => {

    // a constante que recebe os dados do usuário
    const [usuarioDados, setUsuarioDados] = useState<Usuario>();

    // função para pegar o token, decodificar e formatar os dados do usuário
    async function getIdToken() {
        const token = secureLocalStorage.getItem("Token");

        //confere se o token existe
        if (!token) {
            console.log("Token não encontrado")
            return null;
        }

        //decodifica o token e formata os dados do usuário
        try {
            const usuario = jwtDecode(token.toString()) as any;
            console.log(usuario)

            const caminho = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/";
            // o nome das chaves do token são um pouco estranhas, então é necessário colocar o caminho completo para acessar os dados
            const usuarioFormatado = {
                id: usuario[caminho + "nameidentifier"],
                nome: usuario[caminho + "name"],
                email: usuario[caminho + "emailaddress"],
                cargo: usuario["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
                nif: usuario["NIF"]
            };

            console.log(usuarioFormatado);

            setUsuarioDados(usuarioFormatado);
        } catch (error: any) {
            console.log(error.message)
        }
    }

    useEffect(() => {
        getIdToken();
    }, [])


    return (
        <header className={styles.topbar}>
            <nav className={`${styles.navbar} ${styles.layout_guide}`} aria-label="Menu principal">
                <a href="#" className={styles["logo-link"]} aria-label="Página inicial">
                    <img
                        src="../imgs/Logo Senai.png"
                        alt="Logo SENAI"
                        className={styles.logo}
                    />
                </a>

                <ul className={styles["menu-list"]}>
                    <li>
                        <a href="#" className={styles["menu-link"]}>
                            Local
                            <i className="fa-solid fa-chevron-down" />
                        </a>
                    </li>
                    <li>
                        <a href="#" className={styles["menu-link"]}>
                            Patrimônios
                        </a>
                    </li>
                </ul>

                <section className={styles["user-area"]} aria-label="Informações do usuário">
                    <button className={styles["user-icon"]} aria-label="Abrir perfil do usuário">
                        <i className="fa-solid fa-user" />
                    </button>

                    <div className={styles["user-info"]}>
                        <strong>{usuarioDados?.nome}</strong>
                        <span>{usuarioDados?.email}</span>
                    </div>

                    <button className={styles["arrow-button"]} aria-label="Abrir opções da conta">
                        <i className="fa-solid fa-chevron-down" />
                    </button>
                </section>

                <button className={styles.hamburguer} aria-label="Abrir opções de menu">
                    <i className="fa-solid fa-bars" />
                </button>
            </nav>
        </header>
    );
};

export default Header;