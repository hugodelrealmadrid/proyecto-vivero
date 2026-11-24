export async function request(path, options = {}) {
  let response;
  try {
    response = await fetch(path, {
      credentials: "same-origin",
      ...options,
      headers: { "Content-Type": "application/json", ...options.headers },
    });
  } catch {
    throw new Error(
      "No se pudo conectar. Comprueba que el servidor esté en ejecución.",
    );
  }
  const data = await response
    .json()
    .catch(() => ({ error: "Respuesta del servidor no válida." }));
  if (!response.ok) {
    const error = new Error(data.error || "No se pudo completar la operación.");
    error.status = response.status;
    throw error;
  }
  return data;
}
