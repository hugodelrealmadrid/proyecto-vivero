const paths = {
  plant: (
    <path d="M12 21V11M12 15C4 16 2 10 3 4c6 0 10 3 9 11ZM12 12c0-6 4-9 9-9 1 6-2 10-9 9Z" />
  ),
  home: <path d="m3 10 9-7 9 7v11H3ZM9 21v-8h6v8" />,
  cart: (
    <>
      <path d="M2 3h3l3 13h11l3-9H6" />
      <circle cx="9" cy="20" r="1" />
      <circle cx="18" cy="20" r="1" />
    </>
  ),
  truck: (
    <>
      <path d="M2 5h12v12H2ZM14 10h4l4 4v3h-8" />
      <circle cx="6" cy="18" r="2" />
      <circle cx="18" cy="18" r="2" />
    </>
  ),
  box: <path d="m12 2 10 5v11l-10 5-10-5V7ZM2 7l10 5 10-5M12 12v11M7 4l10 5" />,
  tools: (
    <path d="m3 3 18 18M3 21 14 10M17 3a5 5 0 0 0-5 6l3 3a5 5 0 0 0 6-5l-4 2-2-2Z" />
  ),
  people: (
    <>
      <circle cx="9" cy="7" r="4" />
      <path d="M2 21v-3a7 7 0 0 1 14 0v3ZM17 3a4 4 0 0 1 0 8M19 15c3 1 3 3 3 6" />
    </>
  ),
  search: (
    <>
      <circle cx="10" cy="10" r="7" />
      <path d="m15 15 6 6" />
    </>
  ),
  bell: <path d="M4 17h16l-2-4V8a6 6 0 0 0-12 0v5ZM10 21h4" />,
  user: (
    <>
      <circle cx="12" cy="8" r="4" />
      <path d="M4 22v-3a8 8 0 0 1 16 0v3" />
    </>
  ),
  help: (
    <>
      <circle cx="12" cy="12" r="10" />
      <path d="M9 9a3 3 0 0 1 6 0c0 2-3 2-3 5M12 18h.01" />
    </>
  ),
  activity: <path d="M2 12h4l3-8 5 16 3-8h5" />,
  chevron: <path d="m8 10 4 4 4-4" />,
  close: <path d="m6 6 12 12M6 18 18 6" />,
};
export default function Icon({ name = "plant", ...props }) {
  return (
    <svg viewBox="0 0 24 24" aria-hidden="true" {...props}>
      {paths[name] || paths.plant}
    </svg>
  );
}
