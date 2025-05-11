"use client";

import { useEffect } from "react";

function DisableContextMenu() {
  useEffect(() => {
    const handleContextMenu = (event: Event) => {
      event.preventDefault();
    };

    document.addEventListener("contextmenu", handleContextMenu);

    return () => {
      document.removeEventListener("contextmenu", handleContextMenu);
    };
  }, []);

  return null;
}

export default DisableContextMenu;
