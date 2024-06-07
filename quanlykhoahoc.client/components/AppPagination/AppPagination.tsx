import { Pagination } from "@mantine/core";
import { useRouter, useSearchParams } from "next/navigation";

type AppPaginationProps = {
  total: number | undefined;
  page: number | undefined;
};

export default function AppPagination({ total, page }: AppPaginationProps) {
  const searchParams = useSearchParams();
  const router = useRouter();

  const handleChange = (newPage: number) => {
    const url = new URLSearchParams(searchParams.toString());
    if (url.get("commentId")) {
      url.delete("commentId");
    }
    url.set("page", newPage.toString());

    router.push(`?${url.toString()}`);
  };

  if (!total || total <= 1) return null;

  return (
    <Pagination
      withEdges
      total={total}
      onChange={handleChange}
      value={page}
    />
  );
}
