import { useEffect, useRef } from "react";
import Icon from "./Icon.jsx";
export default function Modal({ title, children, onClose }) {
  const ref = useRef(null);
  useEffect(() => {
    const el = ref.current;
    el.showModal();
    return () => el.close();
  }, []);
  return (
    <dialog ref={ref} onCancel={onClose} aria-labelledby="dialog-title">
      <div className="dialog-heading">
        <h2 id="dialog-title">{title}</h2>
        <button onClick={onClose} className="icon-button" aria-label="Cerrar">
          <Icon name="close" />
        </button>
      </div>
      <div id="dialog-content">{children}</div>
    </dialog>
  );
}
