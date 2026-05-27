import { useState } from "react";
import styles from "./login.module.css";
import { useRouter } from "next/navigation";
import { toast } from "react-toastify";
import { login } from "../api/authService";
import { sucesso } from "@/utils/toast";

const Login = () => {

    const [nif, setNif] = useState("");
    const [senha, setSenha] = useState("");

    const router = useRouter();
    const sucess = (msg: string) => toast.success(msg);
    const erro = (msg: string) => toast.error(msg);

    async function autenticar(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        try {
            await login(nif, senha);
            sucesso("Login realizado com sucesso!");
            router.push("/local");
        } catch (error: any) {
            erro(error.message);
        }
    }

    return (
        <>
            <main className={styles.loginPage}>
                <section className={styles.loginBanner} aria-label="Apresentação do sistema">
                    <img src="../imgs/Imagem do login.png" alt="Imagem de fundo relacionada à tecnologia" className={styles.bannerImage} />
                    <div className={styles.bannerOverlay} />
                    <div className={styles.bannerContent}>
                        <img src="../imgs/Logo Senai.png" alt="Logo do SENAI" className={styles.senaiLogo} />
                        <h2>Gestão de patrimônios</h2>
                        <p className={styles.bannerContentText}>
                            Controle, organização e transparência do patrimônio com eficiência.
                        </p>
                        <p />
                    </div>
                </section>
                <section className={styles.loginArea} aria-label="Formulário de login">
                    <form className={styles.loginForm} onSubmit={autenticar}>
                        <h1>Login</h1>
                        <div className={styles.formGroup}>
                            <label htmlFor="nif">NIF:</label>
                            <input type="text" id="nif" name="nif" placeholder="Insira o seu NIF"
                                required value={nif} onChange={(e) => setNif(e.target.value)} />
                        </div>
                        <div className={styles.formGroup}>
                            <label htmlFor="senha">Senha:</label>
                            <div className={styles.passwordField}>
                                <input type="password" id="senha" name="senha" placeholder="Insira a sua senha"
                                    required value={senha} onChange={(e) => setSenha(e.target.value)} />
                                <button type="button" className={styles.showPassword} aria-label="Mostrar senha">
                                    👁
                                </button>
                            </div>
                        </div>
                        <button type="submit" className={styles.loginButton}>
                            Entrar
                        </button>
                    </form>
                </section>
            </main>
        </>
    );
}
export default Login
