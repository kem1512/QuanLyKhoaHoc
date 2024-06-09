"use client";

import { Alert, Center, SimpleGrid } from "@mantine/core";
import useSWR from "swr";
import { BlogClient } from "../web-api-client";
import { useQuery } from "../../lib/helper";
import Loading from "../../components/Loading/Loading";
import AppPagination from "../../components/AppPagination/AppPagination";
import { BlogCard } from "../../components/Card/BlogCard/BlogCard";
import RootLayout from "../../components/Layout/RootLayout/RootLayout";

export default function Blog() {
  const BlogService = new BlogClient();

  const query = useQuery();

  const { data, isLoading } = useSWR(
    `/api/blog/${new URLSearchParams(query as any)}`,
    () =>
      BlogService.getEntities(
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 9
      )
  );

  return (
    <RootLayout>
      {isLoading ? (
        <Loading />
      ) : data?.items?.length && data.items.length >= 0 ? (
        <>
          <SimpleGrid spacing={"xs"}>
            {data.items.map((item) => {
              return <BlogCard key={item.id} data={item} />;
            })}
          </SimpleGrid>
          <Center>
            <AppPagination page={data.pageNumber} total={data.totalPages} />
          </Center>
        </>
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </RootLayout>
  );
}
