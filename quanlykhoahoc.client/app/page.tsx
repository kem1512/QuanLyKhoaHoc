"use client";

import { Alert, Center, SimpleGrid } from "@mantine/core";
import { CourseCard } from "../components/Card/CourseCard/CourseCard";
import useSWR from "swr";
import { CourseClient } from "./web-api-client";
import { useQuery } from "../lib/helper";
import Loading from "../components/Loading/Loading";
import AppPagination from "../components/AppPagination/AppPagination";
import RootLayout from "../components/Layout/RootLayout/RootLayout";

export default function Home() {
  const CourseService = new CourseClient();

  const query = useQuery();

  const { data, isLoading } = useSWR(
    `/api/course/${new URLSearchParams(query as any)}`,
    () =>
      CourseService.getEntities(
        null,
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 9
      ),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  return (
    <RootLayout>
      {isLoading ? (
        <Loading />
      ) : data?.items?.length && data.items.length >= 0 ? (
        <>
          <SimpleGrid cols={{ base: 1, lg: 3 }} spacing={"xs"} mb={"sm"}>
            {data.items.map((item) => {
              return <CourseCard key={item.code} data={item} />;
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
