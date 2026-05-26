import { toast, ToastOptions } from "react-toastify";
import styles from "./toast.module.css";

const config: ToastOptions =
{
    className: styles.toast,
};

export const sucesso = (msg: string, callback?: () => void) => { toast.success(msg, { ...config, onClose: callback }); };

export const erro = (msg: string) => { toast.error(msg, config); };

export const toastConfirmarExclusao = (aoConfirmar: () => void) => {
    toast(({ closeToast }) => (
        <div className={styles.confirmacao}>
            <p>Deseja realmente excluir?</p>
            <div className={styles.acoes}>
                <button className={styles.confirmar} onClick={() => { aoConfirmar(); closeToast?.(); }}>Sim</button>

                <button className={styles.cancelar} onClick={() => closeToast?.()}>Cancelar</button>
            </div>
        </div>
    ), { ...config, autoClose: false }
    );
};