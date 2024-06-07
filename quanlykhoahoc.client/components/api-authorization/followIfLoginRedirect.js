import { toast } from "react-toastify";

export default function followIfLoginRedirect(response) {
  if (response.status === 401 && window.location.pathname !== "/login") {
    window.location.href = `/login`;
  }

  if (response.status === 403) {
    toast.warning("Bạn Không Có Quyền Truy Cập");
    return;
  }
}
