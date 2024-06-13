"use client";

import {
  ActionIcon,
  Badge,
  Button,
  Center,
  Flex,
  Input,
  Loader,
  Menu,
  ScrollArea,
  Select,
  Table,
  Text,
} from "@mantine/core";
import useSWR from "swr";
import DashboardLayout from "../Layout/DashboardLayout/DashboardLayout";
import Link from "next/link";
import { IconArrowDown, IconArrowUp, IconDots } from "@tabler/icons-react";
import { modals } from "@mantine/modals";
import { handleSubmit, useQuery, useSort, useFilter } from "../../lib/helper";
import AppPagination from "../../components/AppPagination/AppPagination";
import { useEffect, useState } from "react";
import { FilterProps, types } from "../../lib/type";
import { usePathname } from "next/navigation";

type DataTableProps = {
  url: string;
  fields: any;
  fetchAction: any;
  deleteAction: any;
};

export default function DataTable({
  url,
  fields,
  fetchAction,
  deleteAction,
}: DataTableProps) {
  const query = useQuery();
  const handleSort = useSort();
  const handleFilter = useFilter();
  const pathname = usePathname();

  const [filter, setFilter] = useState<FilterProps>({
    type: types[0].value,
    field: fields[0],
    value: "",
  });

  useEffect(() => {
    if (filter.value) {
      handleFilter("filters", filter.field + filter.type + filter.value);
    } else {
      handleFilter("filters", "");
    }
  }, [filter, handleFilter]);

  var fetch = async () => {
    return await fetchAction(
      query.filters,
      query.sorts,
      query.page ?? 1,
      query.pageSize ?? 10
    );
  };

  const { data, isLoading, mutate } = useSWR(
    `/api${url}${new URLSearchParams(query as any)}`,
    () => fetch(),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const handleDelete = (id: number | undefined) => {
    modals.openConfirmModal({
      title: "Xóa Chủ Đề",
      children: (
        <Text size="sm">
          Bạn Chắc Chắn Muốn Xóa? Thao Tác Này Sẽ Không Thể Phục Hồi
        </Text>
      ),
      confirmProps: { color: "red" },
      labels: { confirm: "Chắc Chắn", cancel: "Hủy" },
      onConfirm: () =>
        handleSubmit(() => {
          deleteAction(id).then(() => {
            mutate();
          });
        }, "Xóa Thành Công"),
    });
  };

  return (
    <DashboardLayout>
      <Flex my={"sm"} justify={"end"} align={"center"} gap={"xs"}>
        <Link href={"/dashboard/blog"}>
          <Button color="red" size="xs">
            Clear
          </Button>
        </Link>
        <Input
          placeholder="Nhập Nội Dung Tìm Kiếm"
          value={filter.value}
          onChange={(e) =>
            setFilter((prev) => ({ ...prev, value: e.target.value }))
          }
        />
        <Select
          defaultValue={types.find((c) => c.value === filter.type)?.label}
          data={types.map((item) => item.label)}
          onChange={(e) => {
            const selectedType = types.find((c) => c.label === e)?.value;
            if (selectedType) {
              setFilter((prev) => ({
                ...prev,
                type: selectedType,
              }));
            }
          }}
        />
        <Select
          value={filter.field}
          data={fields}
          onChange={(e) => {
            if (e) {
              setFilter((prev) => ({ ...prev, field: e }));
            }
          }}
        />
        <Link href={`${pathname}/create`}>
          <Button ms={"auto"} size="xs">
            Tạo Mới
          </Button>
        </Link>
      </Flex>
      <ScrollArea>
        <Table>
          <Table.Thead>
            <Table.Tr>
              {fields.map((item) => (
                <Table.Th onClick={() => handleSort(item)} key={item}>
                  <Flex justify={"space-between"}>
                    {item}
                    {query.sorts?.includes(`-${item}`) ? (
                      <IconArrowDown width={13} />
                    ) : (
                      <IconArrowUp width={13} />
                    )}
                  </Flex>
                </Table.Th>
              ))}
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {!isLoading ? (
              data?.items?.length && data.items.length >= 0 ? (
                data.items.map((item) => (
                  <Table.Tr key={item.id}>
                    {fields.map((field) => {
                      const value = item[field];
                      return (
                        <Table.Td
                          key={field}
                          py={"md"}
                          style={{
                            wordBreak: "break-word",
                          }}
                        >
                          {value instanceof Date ? (
                            value.toDateString()
                          ) : typeof value === "boolean" ? (
                            <Badge color={value === true ? "blue" : "red"}>
                              {value ? "Đang Kích Hoạt" : "Không Kích Hoạt"}
                            </Badge>
                          ) : (
                            value ?? "Trống"
                          )}
                        </Table.Td>
                      );
                    })}

                    <Table.Td>
                      <Menu shadow="md">
                        <Menu.Target>
                          <ActionIcon variant="transparent">
                            <IconDots />
                          </ActionIcon>
                        </Menu.Target>
                        <Menu.Dropdown>
                          <Link
                            href={`/dashboard/${url}/${item.id}`}
                            style={{ textDecoration: "none" }}
                          >
                            <Menu.Item>Sửa</Menu.Item>
                          </Link>
                          <Menu.Item onClick={() => handleDelete(item.id)}>
                            Xóa
                          </Menu.Item>
                        </Menu.Dropdown>
                      </Menu>
                    </Table.Td>
                  </Table.Tr>
                ))
              ) : (
                <Table.Tr>
                  <Table.Td colSpan={fields.length}>
                    <Center py={"sm"}>Không Có Gì Ở Đây Cả</Center>
                  </Table.Td>
                </Table.Tr>
              )
            ) : (
              <Table.Tr>
                <Table.Td colSpan={fields.length}>
                  <Center py={"sm"}>
                    <Loader size={"sm"} />
                  </Center>
                </Table.Td>
              </Table.Tr>
            )}
          </Table.Tbody>
        </Table>
      </ScrollArea>
      <Flex py={"xs"} justify={"space-between"}>
        <AppPagination page={data?.pageNumber} total={data?.totalPages} />
        <Select
          ms={"auto"}
          data={["5", "10", "15", "20", "25"]}
          defaultValue={query.pageSize ?? "10"}
          onChange={(e) => handleFilter("pageSize", e)}
        />
      </Flex>
    </DashboardLayout>
  );
}
