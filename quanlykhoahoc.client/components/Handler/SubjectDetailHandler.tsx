"use client";

import { Checkbox, Group, SimpleGrid, TextInput } from "@mantine/core";
import {
  SubjectDetailClient,
  SubjectDetailCreate,
  SubjectDetailUpdate,
  ISubjectDetailMapping,
} from "../../app/web-api-client";
import { handleSubmit, useQuery } from "../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import ActionButton from "../../components/Helper/ActionButton";
import AppPagination from "../AppPagination/AppPagination";

export default function SubjectDetailHandler({
  subjectId,
}: {
  subjectId?: number;
}) {
  const SubjectDetailService = new SubjectDetailClient();

  const query = useQuery();

  const { data, mutate } = useSWR(
    `/api/subjectDetail/${subjectId}/${new URLSearchParams(query)}`,
    () =>
      SubjectDetailService.getEntities(
        subjectId,
        query.filters,
        query.sorts,
        Number(query.page ?? 1),
        Number(query.pageSize ?? 5)
      ),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [subjectDetails, setSubjectDetails] = useState<ISubjectDetailMapping[]>(
    []
  );
  const [newSubjectDetail, setNewSubjectDetail] =
    useState<ISubjectDetailMapping>({
      name: "",
      linkVideo: "",
      isActive: true,
      isFinished: true,
      subjectId: subjectId,
    });

  const handleChange = (
    index: number,
    key: string,
    value: string | boolean
  ) => {
    const updatedSubjectDetails = [...subjectDetails];
    updatedSubjectDetails[index] = {
      ...updatedSubjectDetails[index],
      [key]: value,
    };
    setSubjectDetails(updatedSubjectDetails);
  };

  const handleSave = (index: number) => {
    const subjectDetail = subjectDetails[index];
    handleSubmit(
      () => {
        return subjectDetail.id
          ? SubjectDetailService.updateEntity(
              subjectDetail.id,
              subjectDetail as SubjectDetailUpdate
            )
          : SubjectDetailService.createEntity(
              subjectDetail as SubjectDetailCreate
            );
      },
      `${subjectDetail.id ? "Sửa" : "Thêm"} Thành Công`,
      mutate
    );
  };

  const handleNewSave = () => {
    handleSubmit(
      () =>
        SubjectDetailService.createEntity(
          newSubjectDetail as SubjectDetailCreate
        ),
      "Thêm Thành Công",
      mutate
    ).then(() => {
      setNewSubjectDetail({
        name: "",
        linkVideo: "",
        isActive: true,
        isFinished: true,
        subjectId: subjectId,
      });
    });
  };

  const handleNewChange = (key: string, value: string | boolean) => {
    setNewSubjectDetail((prev) => ({ ...prev, [key]: value }));
  };

  useEffect(() => {
    if (data?.items) {
      setSubjectDetails(data.items);
    }
  }, [data]);

  if (subjectId == null) return null;

  return (
    <>
      {subjectDetails.map((item, index) => {
        return (
          <SimpleGrid cols={{ base: 1, lg: 4 }}>
            <TextInput
              label="Tiêu Đề"
              placeholder="Nhập Tiêu Đề"
              value={item.name}
              onChange={(e) => handleChange(index, "name", e.target.value)}
              labelProps={{ style: { marginBottom: 6 } }}
            />
            <TextInput
              label="Đường Dẫn Video"
              placeholder="Nhập Đường Dẫn Video"
              value={item.linkVideo}
              onChange={(e) => handleChange(index, "linkVideo", e.target.value)}
              labelProps={{ style: { marginBottom: 6 } }}
            />
            <Group justify="space-around">
              <Checkbox
                label="Kích Hoạt"
                mt="auto"
                mb="6"
                checked={item.isActive}
                onChange={(e) =>
                  handleChange(index, "isActive", e.target.checked)
                }
              />
              <Checkbox
                label="Đã Hoàn Thành"
                mt="auto"
                mb="6"
                checked={item.isFinished}
                onChange={(e) =>
                  handleChange(index, "isFinished", e.target.checked)
                }
              />
            </Group>
            <Group>
              <ActionButton
                mt="auto"
                mb="6"
                size="xs"
                action={() => handleSave(index)}
              >
                Xác Nhận
              </ActionButton>
              <ActionButton
                mt="auto"
                mb="6"
                size="xs"
                color="red"
                action={() =>
                  handleSubmit(
                    () => SubjectDetailService.deleteEntity(item.id),
                    "Xóa Thành Công",
                    mutate
                  )
                }
              >
                Xóa
              </ActionButton>
            </Group>
          </SimpleGrid>
        );
      })}
      <SimpleGrid cols={4}>
        <TextInput
          label="Tiêu Đề"
          placeholder="Nhập Tiêu Đề"
          value={newSubjectDetail.name}
          onChange={(e) => handleNewChange("name", e.target.value)}
          labelProps={{ style: { marginBottom: 6 } }}
        />

        <TextInput
          label="Đường Dẫn Video"
          placeholder="Nhập Đường Dẫn Video"
          value={newSubjectDetail.linkVideo}
          onChange={(e) => handleNewChange("linkVideo", e.target.value)}
          labelProps={{ style: { marginBottom: 6 } }}
        />

        <Group justify="space-around">
          <Checkbox
            label="Kích Hoạt"
            mt="auto"
            mb="6"
            checked={newSubjectDetail.isActive}
            onChange={(e) => handleNewChange("isActive", e.target.checked)}
          />

          <Checkbox
            label="Đã Hoàn Thành"
            mt="auto"
            mb="6"
            checked={newSubjectDetail.isFinished}
            onChange={(e) => handleNewChange("isFinished", e.target.checked)}
          />
        </Group>

        <ActionButton mt="auto" mb="6" me="auto" size="xs" action={handleNewSave}>
          Xác Nhận
        </ActionButton>
        <AppPagination page={data?.pageNumber} total={data?.totalPages} />
      </SimpleGrid>
    </>
  );
}
