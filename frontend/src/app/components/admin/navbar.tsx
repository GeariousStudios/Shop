import React, { ReactNode } from "react";
import styles from "./navbar.module.css";
import "@/app/styles/admin/buttons.css";
import { useRouter } from "next/navigation";
import { useAuth } from "@/app/context/useAuth";
import { Box, ChartColumnStacked, User, ExternalLink } from "lucide-react";

interface NavbarProps {
  children: ReactNode;
}

const Navbar: React.FC<NavbarProps> = ({ children }) => {
  const { user, logoutUser } = useAuth();
  const router = useRouter();

  const handleLogout = () => {
    logoutUser();
    router.push("/pages/admin/login");
  };

  return (
    <>
      {/* Top bar */}
      <div className={styles.navbarTop}>
        <ul className={styles.topListRight}>
          <li>
            <p>
              Välkommen <b>{user?.email}</b>!
            </p>
          </li>
          <User className={styles.userIcon} onClick={() => router.push("/pages/admin/settings")} />
        </ul>

        <ul className={styles.topListLeft}>
          <li onClick={() => router.push("/pages/admin/settings")}>
            <ExternalLink className={styles.openShopIcon} />
            Se din butik
          </li>
        </ul>
      </div>

      {/* Sidebar */}

      <div className={styles.navbarSide}>
        <ul className={styles.sideList}>
          <img
            src="/images/illuminealogo.svg"
            alt="Illuminea Logo"
            className={styles.logo}
            onClick={() => router.push("/pages/admin/dashboard")}
          />
          <h6>HANTERA</h6>
          <li className={styles.hoverable} onClick={() => router.push("/pages/admin/add-product")}>
            <Box className={styles.sideListIcon} />
            Produkter
          </li>
          <li className={styles.hoverable} onClick={() => router.push("/pages/admin/add-product")}>
            <ChartColumnStacked className={styles.sideListIcon} />
            Kategorier
          </li>
        </ul>
      </div>

      {/* Content */}
      <div className={styles.content}>{children}</div>
    </>
  );
};

export default Navbar;
